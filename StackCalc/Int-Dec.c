/*
    Author: Tarasenko Nikita
    Problem: "StackCalc", Int-Dec.c
 
 */

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
            a -> sign--;
            result = Dec(b, a);
        }
        else
        {
            b -> sign--;
            result = Dec(a, b);
        }
    }
    else
    {
        IntList *c;
        c = (IntList*)malloc(sizeof(IntList));
        
        if(!c)
        {
            fprintf(stdout, "Not enough memory!\n");
            stack = Add(a);
            stack = Add(b);
            memClear();
            exit (NOT_ENOUGH_MEMORY);
        }
        
        c -> next = NULL;
        c -> sign = a -> sign;
        c -> value = 0;
        c -> length = 1;
        
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
                
                if(!newRank)
                {
                   fprintf(stdout, "Not enough memory!\n");
                   stack = Add(a);
                   stack = Add(b);
                   stack = Add(c);
                   memClear();
                   exit (NOT_ENOUGH_MEMORY);
                }
                
                newRank -> next = NULL;
                newRank -> sign = c -> sign;
                newRank -> value = 0;
                result -> length++;
                newRank -> length = result -> length;
                
                c -> next = newRank;
            }
            
            c = c -> next;
        }
    }
    
    return result;
}

IntList* Dec(IntList* a, IntList* b)
{
    IntList *result, *del;
    
    if (a -> sign - b -> sign == 1 || (a -> sign && b -> sign))
    {
        if (a -> sign - b -> sign == 1)
        {
            b -> sign = !b -> sign;
            EditSign(b);
            result = Inc(a, b);
        }
        else if (a -> sign && b -> sign)
        {
            b -> sign--;
            a -> sign--;
            EditSign(a);
            EditSign(b);
            result = Dec(b, a);
        }
    }
    else
    {
        if (a -> length < b -> length || (a -> length == b -> length)?Compare(a, b):0)
        {
            IntList* foo;
            
            foo = a;
            a = b;
            b = foo;
            
            a -> sign = 1;
        }
        
        result = a;
        
        while (a && b)
        {
            addIntList(a, -1 * b -> value);
            del = b -> next;
            free(b);
            b = del;
            a = a -> next;
        }
        if(result -> length > 1)
        {
            EditLength(result, result -> length);
        }
    }
    
    return result;
}
