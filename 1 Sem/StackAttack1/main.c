#pragma check_stack(off)

#include <stdio.h>
#include <string.h>

void right_Func(char*);
void hack_Func();


int main(int argc, char* argv[])
{
    printf("Adress of \"Right_Func\": %p\n", right_Func);
    printf("Adress of \"Hack_Func\": %p\n", hack_Func);
    printf("Adress of \"main\": %p\n", main);
    
    
    char str[] = "00000\x79\x11\x40\x00";
/*
    if (argc != 2)
    {
        printf("Need one argument!");
        return 0;
    }
*/

    right_Func(str);
    
    printf("Done!");
    return 0;
}

void right_Func(char* input)
{
    char buffer[1];
    
/*
    printf("%d\n", sizeof(&main));
*/
    

/*
    for (int i = 0; i < 70; i++) {
        //printf("%ld : %ld\n", i, buffer[i] - (long)&main);
        printf("%p\n", buffer [i]);
    }
*/

    
    //printf("%p\n%p\n%p\n%p\n%p\n%p\n%p\n%p\n%p\n%p\n%p\n\n");
    
    //printf("%s\n", input);
    
    strcpy(buffer[12], "\x73\x11\x40\x00");
    
    //scanf("%d", &buffer[13]);
    
/*
    for (int i = 0; i < 24; i++) {
        //printf("%ld : %ld\n", i, buffer[i] - (long)&main);
        printf("%p\n", buffer [i]);
    }
    
*/
    //printf("%p\n%p\n%p\n%p\n%p\n%p\n%p\n%p\n%p\n%p\n%p\n\n");
    
    //printf("%p", buffer[3]);
}

void hack_Func()
{
    printf("Auch! I've been hacked!!");
}