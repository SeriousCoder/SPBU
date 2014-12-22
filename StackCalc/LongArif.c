/*
    Author: Tarasenko Nikita
    Problem: "StackCalc", LongArif.c
 
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
            printf("Not enough memory!");
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
                   printf("Not enough memory!");
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
    
    if (a -> sign - b -> sign || (a -> sign && b -> sign))
    {
        if (a -> sign - b -> sign)
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
        if (a -> length < b -> length || Compare(a, b))
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

IntList* Mult(IntList* a, IntList* b)
{
    IntList *result, *c, *del, *bm, *now;
    int sign = 0;
    
    if (a -> sign - b -> sign)
    {
            sign++;
    }
    
    c = (IntList*)malloc(sizeof(IntList));
    
    c -> next = NULL;
    c -> sign = sign;
    c -> value = 0;
    c -> length = 1;
    
    result = c;
    now = c;
    bm = b;
    
    while (a)
    {
        while(b)
        {
            addIntList(c, a -> value * b -> value);
            b = b -> next;
            
            if (!c -> next && b)
            {
                IntList *newRank = (IntList*)malloc(sizeof(IntList));
                
                if(!newRank)
                {
                   printf("Not enough memory!");
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
    
}

IntList* Read(char ch, int sign)
{
    IntList *newInt = (IntList*)malloc(sizeof(IntList));
    
    if(!newInt)
    {
        printf("Not enough memory!");
        exit (NOT_ENOUGH_MEMORY);
    }
    
    newInt -> next = NULL;
    newInt -> sign = sign;
    newInt -> length = 1;
    newInt -> value = ch - 48;
    
    ch = getchar();
    
    while(ch != ' ')
    {
        newInt -> length++;
        
        addIntList10(newInt, ch - 48);
        
        ch = getchar();
    }
    
    return newInt;
}

void addIntList10(IntList* list, int value)
{
    int foo = value;
    value = list -> value;
    list -> value = foo;
    
    if (list -> next || value)
    {
        if (!list -> next)
        {
            IntList *newInt = (IntList*)malloc(sizeof(IntList));
            
            if(!newInt)
            {
                printf("Not enough memory!");
                exit (NOT_ENOUGH_MEMORY);
            }
            
            newInt -> next = NULL;
            newInt -> sign = list -> sign;
            list -> next = newInt;
            newInt -> value = 0;
            list -> length++;
            newInt -> length = list -> length;
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
            
            if(!newInt)
            {
                printf("Not enough memory!");
                exit (NOT_ENOUGH_MEMORY);
            }
            
            newInt -> next = NULL;
            newInt -> sign = list -> sign;
            list -> next = newInt;
            newInt -> value = 0;
            list -> length++;
            newInt -> length = list -> length;
        }
        
        addIntList(list -> next, value);
    }
    else if (list -> value < 0)
    {
        
        value = -1;
        
        if(list -> next)
        {
            list -> value = list -> value + 10;
        
            addIntList(list -> next, value);
        }
        else if(list -> value < 0)
        {
            list -> value *= -1;
            list -> sign = !list -> sign;
        }
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
    
    free(value);
}

void EditSign(IntList* value)
{
    IntList* next = value -> next;
    
    while (next)
    {
        next -> sign = value -> sign;
        next = next -> next;
    }
}

void EditLength(IntList* prev, int leng)
{
    IntList* this = prev -> next;
    IntList* next = this -> next;
    
    this -> length = leng;
    
    if(next)
    {
        EditLength(this, leng);
    }
    
    if (!this -> next && this -> value == 0)
    {
        prev -> length = this -> length - 1;
        prev -> next = NULL;
        free(this);
    }
}

int Compare(IntList* a, IntList* b)
{
    int bool = -1;
    
    if(a->next)
    {
        bool = Compare(a -> next, b -> next);
    }
    if(bool == -1)
    {
        return a -> value < b -> value;
    }

    return bool;
}