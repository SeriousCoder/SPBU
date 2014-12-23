#include <stdio.h>
#include <stdlib.h>
#include "List.h"

IntList* Mult(IntList* a, IntList* b)
{
    IntList *result, *c, *del, *bm, *now;
    int sign = 0;
    
    if (a -> sign - b -> sign)
    {
            sign++;
    }
    
    result = (IntList*)malloc(sizeof(IntList)); //условие
    
    result -> next = NULL;
    result -> sign = sign;
    result -> value = 0;
    result -> length = 1;
    
    c = result;
    now = c;
    bm = b;
    
    while (a)
    {
        while(b)
        {
            addIntList(c, a -> value * b -> value);
            result -> length = c -> length;
            b = b -> next;
            
            if (!c -> next && b)
            {
                IntList *newRank = (IntList*)malloc(sizeof(IntList));
                
                if(!newRank)
                {
                   fprintf(stdout, "Not enough memory!\n");
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
        
        b = bm;
        
        now = now -> next;
        c = now;
        
        del = a -> next;
        free(a);
        a = del;
    }
    
    
    while (bm)
    {
        del = bm -> next;
        free(bm);
        bm = del;
    }
    
    return result;
}

IntList* Div(IntList* a, IntList* b)
{
    IntList *result, *del, *bm, *delm;
    int sign = 0;
    int len, foo, bool = 1;
    
    if (a -> sign - b -> sign)
    {
            sign++;
    }
    
    bm = b;
    
    IntList *c;
    c = (IntList*)malloc(sizeof(IntList));
    
    if(!c)
    {
        fprintf(stdout, "Not enough memory!\n");
        exit (NOT_ENOUGH_MEMORY);
    }
    
    c -> next = NULL;
    c -> sign = sign;
    c -> value = 0;
    c -> length = 1;
    
    foo = a -> length - b -> length + 1;

    
    while((a -> length == b -> length)?Compare(a, b):0 || a -> length > b -> length)
    {
        foo--; 
        int i = foo;
        del = a;    
        
        if(i >= 0 && foo < a -> length)
        {
            while(i > 0)
            {
                if(i == 1)
                {
                    delm = del;
                }
                i--;
                del = del -> next;
            }
            
            len = a -> length - foo;
            
            
            if(bool)
            {
                if (Compare(del, b))
                {
                    delm = del;
                }
                else
                {
                    del = delm;
                    len++;
                    foo--;
                }
                
                bool--;
            }
        
            while((len == b -> length)?Compare(del, b):0 || len > b -> length)
            {
                int bar;
                
                i++;
                while(b)
                {
                    addIntList(del, -1 * b -> value);
                    b = b -> next;
                    del = del -> next;
                }
                del = delm;
                b = bm;
                
                bar = a -> length;
                
                if (a -> length > 1) 
                {
                    EditLength(a, a -> length);
                }
                if (bar - a -> length)
                {
                    len -= bar - a -> length;
                }
            }
            
            addIntList10(c, i);
        }
    }
    
    while(foo > 0)
    {
        addIntList10(c, 0);
        foo--;
    }
    
    if (a -> sign || b -> sign)
    {
        addIntList(c, 1);
    }
    
    while (a)
    {
        del = a -> next;
        free(a);
        a = del;
    }
    
    while (bm)
    {
        del = bm -> next;
        free(bm);
        bm = del;
    }
    
    
    if(c -> length > 1)
    {
        EditLength(c, c -> length);
    }
    
    return c;
}
