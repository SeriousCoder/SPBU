#include <stdio.h>
#include <stdlib.h>
#include "List.h"


IntList* Inc(IntList* a, IntList* b)
{
    IntList *result, *del;
    
    if (a -> sign - b -> sign)
    {
        if (a->sign)
        {
            result = Dec(b, a);
        }
        else
        {
            result = Dec(a, b);
        }
    }
    else
    {
        IntList *c;
        c = (IntList*)malloc(sizeof(IntList));
        
        c -> next = NULL;
        c -> sign = a -> sign;
        c -> value = 0;
        
        result = c;
        
        while (a || b)
        {
            if (!a)
            {
                addIntList(c, b -> value);
                del = b -> next;
                free(b);
                b = del;
            }
            else if (!b)
            {
                addIntList(c, a -> value);
                del = a -> next;
                free(a);
                a = del;
            }
            else
            {
                addIntList(c, a -> value + b -> value);
                del = a -> next;
                free(a);
                a = del;
                del = b -> next;
                free(b);
                b = del;
            }
            
            if (!c -> next && (a || b))
            {
                IntList *newRank = (IntList*)malloc(sizeof(IntList));
                
                newRank -> next = NULL;
                newRank -> sign = c -> sign;
                newRank -> value = 0;
                
                c -> next = newRank;
            }
            
            c = c -> next;
        }
    }
    
    return result;
}

IntList* Dec(IntList* a, IntList* b)
{
    
}

IntList* Mult(IntList* a, IntList* b)
{
    
}

IntList* Div(IntList* a, IntList* b)
{
    
}

IntList* Read(char ch, int sign)
{
    IntList *newInt = (IntList*)malloc(sizeof(IntList));
    
    newInt -> next = NULL;
    newInt -> sign = sign;
    newInt -> value = ch - 48;
    
    ch = getchar();
    
    while(ch != ' ')
    {
        addIntList10(newInt, ch - 48);
        
        ch = getchar();
    }
    
    return newInt;
}

void addIntList10(IntList* list, int value)
{
    list -> value = list -> value * 10 + value;
    
    if (list -> value > 9)
    {
        value = list -> value / 10;
        list -> value = list -> value % 10;
        
        if (!list -> next)
        {
            IntList *newInt = (IntList*)malloc(sizeof(IntList));
            
            newInt -> next = NULL;
            newInt -> sign = list -> sign;
            list -> next = newInt;
            newInt -> value = 0;
        }
        
        addIntList10(list -> next, value);
    }
}

void addIntList(IntList* list, int value)
{
    list -> value = list -> value + value;
    
    if (list -> value > 9)
    {
        value = list -> value / 10;
        list -> value = list -> value % 10;
        
        if (!list -> next)
        {
            IntList *newInt = (IntList*)malloc(sizeof(IntList));
            
            newInt -> next = NULL;
            newInt -> sign = list -> sign;
            list -> next = newInt;
            newInt -> value = 0;
        }
        
        addIntList(list -> next, value);
    }
}

void ShowInt(IntList* value, int sign)
{
    if (sign)
    {
        printf("-");
        sign--;
    }
    
    if (value -> next)
    {
        ShowInt(value -> next, sign);
    }
    
    printf("%d", value -> value);
}