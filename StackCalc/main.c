/*
    Author: Tarasenko Nikita
    Problem: "StackCalc", main.c
 
 */

#include <stdio.h>
#include <stdlib.h>
#include "List.h"




StackList* Remote(int show);

void freeInt(IntList *value);

int main(int argc, char **argv) 
{
    FILE *input = NULL, *output = NULL;
    
    stack = NULL;
    char ch;
    
    if(argc == 3)
    {
        input = freopen(argv[1], "r", stdin);
        output = freopen(argv[2], "w", stdout);
        if(!input || !output)
        {
            fprintf(stderr, "File IO error\n");
            return -1;
        }
    }
    else
    {
        fprintf(stderr, "Don't enter input and output files\n");
        return -1;
    }
    
    ch = getchar();
    
    while (ch != EOF)
    {
        if (ch == '*' || ch == '/' || ch == '+')
        {
            if(!stack)
            {
                fprintf(stdout, "Not enough arguments\n");
                memClear();
                return 1;
            }
            
            IntList* a = stack -> integer;
            stack = Remote(0);
            
            if(!stack)
            {
                fprintf(stdout, "Not enough arguments\n");
                stack = Add(a);
                memClear();
                return 1;
            }
            
            IntList* b = stack -> integer;
            stack = Remote(0);
            
            switch(ch)
            {
                case '*':
                    stack = Add(Mult(a, b));
                    break;
                case '/':
                    if(b -> length == 1 && !b->value)
                    {
                        fprintf(stdout, "Division by zero\n");
                        memClear();
                        return 1;
                    }
                    stack = Add(Div(a, b));
                    break;
                case '+':
                    stack = Add(Inc(a, b));
                    break;
            }
            
            if (!stack)
            {
                return 0;
            }
        }
        else if(ch == '-')
        {
            ch = getchar();
            
            if (ch == '\n' || ch == EOF)
            {
                if(!stack)
                {
                    fprintf(stdout, "Not enough arguments\n");
                    memClear();
                    return 1;
                }
                
                IntList* a = stack -> integer;
                stack = Remote(0);
                
                if(!stack)
                {
                    fprintf(stdout, "Not enough arguments\n");
                    stack = Add(a);
                    memClear();
                    return 1;
                }
            
                IntList* b = stack -> integer;
                stack = Remote(0);
            
                stack = Add(Dec(a, b));
            }
            else if (ch >= 48 && ch < 58)
            {
                stack = Add(Read(ch, 1));
            }
            
            if (!stack)
            {
                return 0;
            }
        }
        else if (ch >= 48 && ch < 58)
        {
            stack = Add(Read(ch, 0));
            
            if (!stack)
            {
                return 0;
            }
        }
        else if(ch == '=')
        {
            if(!stack)
            {
                fprintf(stdout, "Not enough arguments\n");
                memClear();
                return 1;
            }
            
            ShowInt(stack -> integer, stack -> integer -> sign, 0);
            printf("\n");
        }
        else if(ch != 13 && ch != '\n')
        {
            fprintf(stdout, "Unknown command\n");
            memClear();
            return 1;
        }

        ch = getchar();
    }
    
    //ShowInt(stack -> integer, stack -> integer -> sign);
    
    if(stack)
    {
        printf("[");
        while(stack)
        {
            stack = Remote(1);
        }
        printf("]");
    }

    return 0;
}

StackList* Add(IntList* value)
{
    StackList *new  = (StackList*)malloc(sizeof(StackList));
    
    if(!new || !value)
    {
        printf("Not enough memory!\n");
        memClear();
        exit (NOT_ENOUGH_MEMORY);
    }
    
    new -> next = stack;
    new -> integer = value;
    
    return new;
}

StackList* Remote(int show)
{
    StackList *next = stack -> next;
    
    IntList* Int = stack -> integer;
    
    if(show)
    {
        ShowInt(Int, Int -> sign, 1);
        if(next)
        {
            printf(", ");
        }
        //next = top -> integer -> next;
        //free(top -> integer);
        
        free(stack);
    }
    
    return next;
}

void memClear()
{
    while(stack)
    {
        freeInt(stack -> integer);
        stack = Remote(0);
    }
}

void freeInt(IntList *value)
{
    if(value -> next)
    {
        freeInt(value -> next);
    }
    
    free(value);
}
