/*
    Author: Tarasenko Nikita
    Problem: "StackCalc", main.c
 
 */

#include <stdio.h>
#include <stdlib.h>
#include "List.h"


StackList* Add(StackList* top, IntList* value);

StackList* Remote(StackList* top, int show);

int main(int argc, char **argv) 
{
    FILE *input = NULL, *output = NULL;
    
    StackList* stack = NULL;
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
                return 1;
            }
            
            IntList* a = stack -> integer;
            stack = Remote(stack, 0);
            
            if(!stack)
            {
                fprintf(stdout, "Not enough arguments\n");
                return 1;
            }
            
            IntList* b = stack -> integer;
            stack = Remote(stack, 0);
            
            switch(ch)
            {
                case '*':
                    stack = Add(stack, Mult(a, b));
                    break;
                case '/':
                    if(b -> length == 1 && !b->value)
                    {
                        fprintf(stdout, "Division by zero\n");
                        return 1;
                    }
                    stack = Add(stack, Div(a, b));
                    break;
                case '+':
                    stack = Add(stack, Inc(a, b));
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
                    return 1;
                }
                
                IntList* a = stack -> integer;
                stack = Remote(stack, 0);
                
                if(!stack)
                {
                    fprintf(stdout, "Not enough arguments\n");
                    return 1;
                }
            
                IntList* b = stack -> integer;
                stack = Remote(stack, 0);
            
                stack = Add(stack, Dec(a, b));
            }
            else if (ch >= 48 && ch < 58)
            {
                stack = Add(stack, Read(ch, 1));
            }
            
            if (!stack)
            {
                return 0;
            }
        }
        else if (ch >= 48 && ch < 58)
        {
            stack = Add(stack, Read(ch, 0));
            
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
                return 1;
            }
            
            ShowInt(stack -> integer, stack -> integer -> sign, 0);
            printf("\n");
        }
        else if(ch != 13 && ch != '\n')
        {
            fprintf(stdout, "Unknown command\n");
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
            stack = Remote(stack, 1);
        }
        printf("]");
    }

    return 0;
}

StackList* Add(StackList* top, IntList* value)
{
    StackList *new  = (StackList*)malloc(sizeof(StackList));
    
    if(!new || !value)
    {
        printf("Not enough memory!\n");
        exit (NOT_ENOUGH_MEMORY);
    }
    
    new -> next = top;
    new -> integer = value;
    
    return new;
}

StackList* Remote(StackList* top, int show)
{
    StackList *stack = top -> next;
    
    IntList* next = top -> integer;
    
    if(show)
    {
        ShowInt(next, next -> sign, 1);
        if(stack)
        {
            printf(", ");
        }
        //next = top -> integer -> next;
        //free(top -> integer);
        
        free(top);
    }
    
    return stack;
}