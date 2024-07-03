echo %data%
cd /d %~dp0

git add *.cs *.bat
git commit -m "%date% "

git push
pause