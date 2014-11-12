#pragma check_stack(off)

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

void right_Func(char* input)
{
    char buffer[4];
    
    
    
    strcpy(buffer, input);
 }

void hack_Func()
{
    printf("Auch! I've been hacked!!");
	exit(0);
	
}

int main(int argc, char* argv[])
{
	int *hack = (int*) hack_Func; 

	char str[] = "000000000000\x95\x11\x41\x00";

    printf("Adress of \"Right_Func\": %p\n", right_Func);
    printf("Adress of \"Hack_Func\": %p\n", hack_Func);
    
	

    /*if (argc != 2)
    {
        printf("Need one argument!");
        return;
    }*/
    
	right_Func(str);

	printf("All right!");
	return 0;
}

