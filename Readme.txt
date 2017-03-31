�⺻ ��ɾ�

###git init

�������丮 �ʱ�ȭ *.git ���� ���� �� ���� ������ ���õ� �������丮 ���� ���� ����
###git status

Git Repository ���� Ȯ��
###git add

Git Stage ������ �ش� ���� �߰�
ex) git add fileName.txt
###git commit

Git Stage ���� ���� -> Git Repository�� �ݿ�
ex) git commit -m "insert Commit message"
-m : commit ���� �ʿ��� ���� ��� ���
--amend : ���� commit�� �޽��� ���� ����
-am : git add ��ɾ� ���� commit ����
commit �޽��� ��� ���

ù ��° �� : commit���� ���� ���� ���� �� �ٷ� ����ؼ� �ۼ�
�� ��° �� : ����
�� ��° �� ���� : ���� ���õ� ���� ���� ���
###git push

GitHub�� �ִ� �������丮 ����
###git log

Git Repository�� commit�� �α� Ȯ�� ��ɾ�
ex) git log --pretty=short [fileName.txt]
--pretty=short : log ��½� ù ��° ����ٸ� ǥ��
[fileName.txt] : �ش� ���� ���� �α׸� ǥ��
-p : ����� ���� Ȯ�� ����
--graph : ���� �귣ġ�� �б� �Ǵ� ���� ��� �ð��� Ȯ�� ����
###git reflog

���� Repository���� ����� ��� commit �α� Ȯ�� ����
###git diff

working tree, �������� ����, �ֽ� commit ������ ���� ���� Ȯ�� ����
ex) git diff // working tree <-> �������� ���� ���� Ȯ��
ex) git diff HEAD // �ֽ� commit ���� Ȯ��
###git branch

branch ���� �� ���� �귣ġ Ȯ��
ex) git branch // ���� �귣ġ Ȯ��
ex) git branch branchName-A // branchName-A �̸��� �귣ġ ����
###git checkout

branch ����
ex) git checkout branchName-A // branchName-A���� �귣ġ ����
ex) git checkout -b branchName-A // branchName-A ���� �� �귣ġ ����
ex) git checkout - // ������ �귣ġ�� �̵�
###git merge

branch ����
ex) git merge --no-ff feature-A // feature-A �귣ġ�� ����
--no-ff : fast-forward ����� ���� ���� Ŀ�� ����
�����ڷ� : http://blog.naver.com/parkjy76/220308638231
###git reset

commit ���� ���·� ����
ex) git reset --hard insertHash
--hard : Repository �� HEAD, ��������, working tree ���� ���±��� ����
insertHash :�ش� commit�� hash ����
###git rebase -i

commit ����, ���� ����
ex) git rebase -i HEAD~2
���� �귣ġ�� HEAD�� ������ �� ���� ���泻�� �����Ϳ� ǥ��
���� �ڷ� : http://minsone.github.io/git/github-advanced-git-interactive-rebase
���� Repository ���

###git remote add

���� Repository ���
ex) git remote add origin git@github.com:CollectYS/git-tutorial.git
origin : �ĺ���
###git push

���� ���� �������丮 ������ ���� �������丮�� ����
ex) git push -u origin master
-u : ���� �������丮�� �ִ� ���� �귣ġ�� ���°� origin �������丮�� master �귣ġ�� ����
###git clone

���� Repository ��������
ex) git clone git@github.com:github-book/git-tutorial.git
###git branch

ex) git branch -a
���� �������丮 & ���� �������丮 �귣ġ ���� ��� ���
###git checkout

ex) git checkout -b feature-D origin/feature-D
feature-D : ���� �ۼ��ϴ� �귣ġ �̸�
origin/feature-D : GitHub�� ���� �������丮
�����ڷ�

�Ҽ��ڵ����� �̲��� GitHub ��õ��� <������ī ����Ű/���μ�>