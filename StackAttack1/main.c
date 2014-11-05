#pragma check_stack(off)

#include <stdio.h>
#include <string.h>

int main(int argc, char* argv[])
{
    printf("Adress of \"Right_Func\": %p\n", right_Func);
    printf("Adress of \"Hack_Func\": %p\n", hack_Func);
    
    if (argc != 2)
    {
        printf("Need one argument!");
        return;
    }
    
    right_Func(argv[1]);
}

void right_Func(char* input)
{
    char buffer[10];
    
    
    
    strcpy(buffer, input);
}

void hack_Func()
{
    printf("Auch! I've been hacked!!");
}