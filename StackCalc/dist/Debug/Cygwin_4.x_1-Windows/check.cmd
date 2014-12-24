@echo off
set comparison=diff -q -s -i -E -Z -b -w -B
set generator=stackcalc.exe
%generator%  test1.txt  test1_student.txt
%generator%  test2.txt  test2_student.txt
%generator%  test3.txt  test3_student.txt
%generator%  test4.txt  test4_student.txt
%generator%  test5.txt  test5_student.txt
%generator%  test6.txt  test6_student.txt
%generator%  test7.txt  test7_student.txt
%generator%  test8.txt  test8_student.txt
%generator%  test9.txt  test9_student.txt
%generator%  test10.txt  test10_student.txt
%generator%  test11.txt  test11_student.txt
%generator%  test12.txt  test12_student.txt
%generator%  test13.txt  test13_student.txt
%generator%  test14.txt  test14_student.txt
%generator%  test15.txt  test15_student.txt

%comparison% test1_res.txt test1_student.txt
%comparison% test2_res.txt test2_student.txt
%comparison% test3_res.txt test3_student.txt
%comparison% test4_res.txt test4_student.txt
%comparison% test5_res.txt test5_student.txt
%comparison% test6_res.txt test6_student.txt
%comparison% test7_res.txt test7_student.txt
%comparison% test8_res.txt test8_student.txt
%comparison% test9_res.txt test9_student.txt
%comparison% test10_res.txt test10_student.txt
%comparison% test11_res.txt test11_student.txt
%comparison% test12_res.txt test12_student.txt
%comparison% test13_res.txt test13_student.txt
%comparison% test14_res.txt test14_student.txt
%comparison% test15_res.txt test15_student.txt