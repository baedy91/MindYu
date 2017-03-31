기본 명령어

###git init

리포지토리 초기화 *.git 폴더 생성 및 현재 폴더와 관련된 리포지토리 관리 정보 저장
###git status

Git Repository 상태 확인
###git add

Git Stage 영역에 해당 파일 추가
ex) git add fileName.txt
###git commit

Git Stage 영역 파일 -> Git Repository에 반영
ex) git commit -m "insert Commit message"
-m : commit 관련 필요한 내용 기록 기능
--amend : 직전 commit한 메시지 내용 수정
-am : git add 명령어 없이 commit 가능
commit 메시지 기록 방법

첫 번째 줄 : commit으로 인한 변경 내용 한 줄로 요악해서 작성
두 번째 줄 : 공백
세 번째 줄 이후 : 변경 관련된 내용 상세히 기록
###git push

GitHub에 있는 리포지토리 갱신
###git log

Git Repository에 commit된 로그 확인 명령어
ex) git log --pretty=short [fileName.txt]
--pretty=short : log 출력시 첫 번째 요약줄만 표시
[fileName.txt] : 해당 파일 관련 로그만 표시
-p : 변경된 내용 확인 가능
--graph : 토픽 브랜치의 분기 또는 통합 모양 시각적 확인 가능
###git reflog

현재 Repository에서 수행된 모든 commit 로그 확인 가능
###git diff

working tree, 스테이지 영역, 최신 commit 사이의 변경 내용 확인 가능
ex) git diff // working tree <-> 스테이지 영역 차이 확인
ex) git diff HEAD // 최신 commit 차이 확인
###git branch

branch 생성 및 현재 브랜치 확인
ex) git branch // 현재 브랜치 확인
ex) git branch branchName-A // branchName-A 이름의 브랜치 생성
###git checkout

branch 변경
ex) git checkout branchName-A // branchName-A으로 브랜치 변경
ex) git checkout -b branchName-A // branchName-A 생성 및 브랜치 변경
ex) git checkout - // 이전의 브랜치로 이동
###git merge

branch 병합
ex) git merge --no-ff feature-A // feature-A 브랜치와 병합
--no-ff : fast-forward 관계라도 필히 머지 커밋 생성
참고자료 : http://blog.naver.com/parkjy76/220308638231
###git reset

commit 과거 상태로 복원
ex) git reset --hard insertHash
--hard : Repository 의 HEAD, 스테이지, working tree 지정 상태까지 복원
insertHash :해당 commit의 hash 삽입
###git rebase -i

commit 변경, 내용 조작
ex) git rebase -i HEAD~2
현재 브랜치의 HEAD를 포함한 두 개의 변경내역 에디터에 표시
참고 자료 : http://minsone.github.io/git/github-advanced-git-interactive-rebase
원격 Repository 통신

###git remote add

원격 Repository 등록
ex) git remote add origin git@github.com:CollectYS/git-tutorial.git
origin : 식별자
###git push

현재 로컬 리포지토리 내용을 원격 리포지토리에 전송
ex) git push -u origin master
-u : 로컬 리포지토리에 있는 현재 브랜치의 상태가 origin 리포지토리의 master 브랜치로 설정
###git clone

원격 Repository 가져오기
ex) git clone git@github.com:github-book/git-tutorial.git
###git branch

ex) git branch -a
로컬 리포지토리 & 원격 리포지토리 브랜치 정보 모두 출력
###git checkout

ex) git checkout -b feature-D origin/feature-D
feature-D : 새로 작성하는 브랜치 이름
origin/feature-D : GitHub의 원격 리포지토리
참고자료

소셜코딩으로 이끄는 GitHub 실천기술 <오오츠카 히로키/윤인성>