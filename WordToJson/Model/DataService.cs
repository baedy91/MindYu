/****
 * filename   DataService
 * date        17.3.23 
 * update     17.3.29
 * author     baedy91@painone.com
 * brief        DataService JsonParse & FileIO
 ****/

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace WordToJson.Model
{
    /* class DateService
     * date 17.3.23
     * author baedy91@painone.com
     * brief ModelService     
     * */
    public class DataService : IDataService
    {
        private String path = @".\data\";  //저장 경로 지정

        /* return : RelationWord of param's word  
         * param : loadword
         * brief : Jsonfile -> object(WordList) load
         */
        public WordList LoadRelationWord(String loadword)    
        {
            var item = new WordList(loadword, new ObservableCollection<RelationWord>());
            String path = this.path + "word" + loadword + ".json";            

            using (StreamReader file = File.OpenText(path))
            {
                if (file == null) return null;
                JsonSerializer serrializer = new JsonSerializer();
                item = (WordList)serrializer.Deserialize(file, typeof(WordList));
            }
            return item;
        }

        /* return : void
         * param : WordList object
         * brief : object(WordList) -> jsonfile save
         * file : word + wordname + .json
         */
        public void SaveRelationWord(WordList saveWord)
        {
            String path = this.path + "word" + saveWord.Word + ".json";
            
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, saveWord);
            }
            SaveContentsList(saveWord.Word);
        }

        /* return : void
         * param : String newWord
         * brief : appendnewWord in contentslist
         * file : contentsList.json
         */
        public void SaveContentsList(String newWord)
        {
            String path = this.path + "contentsList.json";            
            ObservableCollection<StrWrapper> old = new ObservableCollection<StrWrapper>();

            //데이타가 이미 존재하는지 확인
            String oldjson;
            try
            {
                oldjson = File.ReadAllText(path); // 파일 없으면 catch
                if (!(oldjson == ""))
                {
                    old = JsonConvert.DeserializeObject<ObservableCollection<StrWrapper>>(oldjson);
                }
            }
            catch{ }
            
            try
            {
                List<string> ls = new List<string>();
                foreach (StrWrapper sw in old)
                {
                    ls.Add(sw.InData);
                }
                if (ls.Contains(newWord))
                {
                    return;  //이미 단어가 존재할 경우 추가하지 않고 종료
                }
            }
            catch { }
            //
            
            using (StreamWriter file = File.CreateText(path))
            {
                old.Add(new StrWrapper(newWord));
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, old);
            }
        }

        /* return : StringList
       * param : none
       * brief : LoadContentsList
       * file : contentsList.json
       */
        public StringList LoadContentsList()
        {
            ObservableCollection<StrWrapper> list = new ObservableCollection<StrWrapper>();
            String path = this.path + "contentsList.json";

            if (!File.Exists(path))     //파일 없으면 새로운 항목 생성
            {
                NewContentsList();
            }

            using (StreamReader file = File.OpenText(path))
            {
                    String s = file.ReadToEnd();                                        
                    JsonSerializer serializer = new JsonSerializer();
                    list = JsonConvert.DeserializeObject<ObservableCollection<StrWrapper>>(s);    
            }
            
            StringList sl = new StringList();
            sl.STL = list;
            return sl;
        }

        /* return : bool
         * param : removeWord's index of ContestsList
         * brief : remove word in contentslist
         * file : contentsList.json
         * date : 17.3.30
         */
        public removeResult RemoveContentsList(int index)
        {           
            String path = this.path + "contentsList.json";
            ObservableCollection<StrWrapper> old = new ObservableCollection<StrWrapper>();
            String oldjson;
            removeResult re = new removeResult();
            
            if (File.Exists(path)) // 파일 없으면
            {
                try
                {
                    oldjson = File.ReadAllText(path); 
                    old = JsonConvert.DeserializeObject<ObservableCollection<StrWrapper>>(oldjson);
                }
                catch (FileNotFoundException) 
                {
                    re.Result = false;
                    return re;   
                }
            }

            if(File.ReadAllText(path).ToString().Length <15)
            {
                NewContentsList();
                re.Result = false;
                return re;     
            }

            if (old.Count - 1 < index)
            {
                re.Result = false;
                return re;
            }

            re.RemoveWord = old[index].InData; 
            old.RemoveAt(index);// 목록에서 삭제
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, old);
            }
            File.Delete(this.path + "word" + re.RemoveWord + ".json"); //실제 파일 삭제  
            re.Result = true;
            return re;      
        }       

        /* return : void
       * param : oldSL.STL's index , String newData - UpdateData
       * brief : UpdateContentsList
       * file : contentsList.json
       */
        public bool UpdateContentsList(int index, String newData, out string oldData)
        {
            String path = this.path + "contentsList.json";
            StringList oldSL = LoadContentsList();
            oldData = oldSL.STL[index].InData;
            String oldFile = this.path + "word" + oldData + ".json"; //기존 파일 경로
    
            foreach (StrWrapper sw in oldSL.STL)
            {
                if (sw.InData == newData)
                {
                    return false;
                }
            }

            oldSL.STL[index].InData = newData;
            
            ObservableCollection<StrWrapper> newSaveContents = oldSL.STL;            
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, newSaveContents);
            }
             
            String newFile = this.path + "word" + newData + ".json";
            
            File.Move(oldFile, newFile);
         
            WordList saveWord = LoadRelationWord(newData);
            saveWord.Word = newData;
            using (StreamWriter file = File.CreateText(newFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, saveWord);
            }
            SaveContentsList(saveWord.Word);

            return true;
        }

        /* return : void
       * param : none
       * brief : NewContentsList fileCreate
       * file :
       */
        public void NewContentsList()
        {
            SaveRelationWord(new WordList("새항목", new ObservableCollection<RelationWord>()));            
        }

        public DataService()
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists == false)  //폴더 생성
            {
                di.Create();
            }
        }
    }
}