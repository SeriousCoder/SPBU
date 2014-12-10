#include <stdio.h>
#include <stdlib.h>
#include "List.h"

StackList* Add(StackList* top, IntList* value);

StackList* Remote(StackList* top);

int main() 
{
    StackList* stack = NULL;
    char ch;
    
    ch = getchar();
    
    while (ch != '=')
    {
        if (ch == '*' || ch == '/' || ch == '+')
        {
            IntList* a = stack -> integer;
            stack = Remote(stack);
            IntList* b = stack -> integer;
            stack = Remote(stack);
            
            switch(ch)
            {
                case '*':
                    stack = Add(stack, Mult(a, b));
                    break;
                case '/':
                    stack = Add(stack, Div(a, b));
                    break;
                case '+':
                    stack = Add(stack, Inc(a, b));
                    break;
            }
        }
        else if(ch == '-')
        {
            ch = getchar();
            
            if (ch == ' ' || ch == '\0')
            {
                IntList* a = stack -> integer;
                stack = Remote(stack);
                IntList* b = stack -> integer;
                stack = Remote(stack);
            
                stack = Add(stack, Dec(a, b));
            }
            else if (ch >= 48 && ch < 58)
            {
                stack = Add(stack, Read(ch, 1));
            }
        }
        else if (ch >= 48 && ch < 58)
        {
            stack = Add(stack, Read(ch, 0));
        }
        
        ch = getchar();
    }
    
    ShowInt(stack -> integer, stack -> integer->sign);
    
    free(stack);

    return 0;
}

StackList* Add(StackList* top, IntList* value)
{
    StackList *new  = (StackList*)malloc(sizeof(StackList));
    
    new -> next = top;
    new -> integer = value;
    
    return new;
}

StackList* Remote(StackList* top)
{
    StackList *stack = top -> next;
    
    free(top);
    
    return stack;
}



