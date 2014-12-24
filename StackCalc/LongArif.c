/*
    Author: Tarasenko Nikita
    Problem: "StackCalc", LongArif.c
 
 */

#include <stdio.h>
#include <stdlib.h>
#include "List.h"


int CheckChar(char ch)
{
    return (ch > 47) && (ch < 58);
}

IntList* Read(char ch, int sign)
{
    IntList *newInt = (IntList*)malloc(sizeof(IntList));
    
    if(!newInt)
    {
        fprintf(stdout, "Not enough memory!\n");
        memClear();
        exit (NOT_ENOUGH_MEMORY);
    }
    
    newInt -> next = NULL;
    newInt -> sign = sign;
    newInt -> length = 1;
    newInt -> value = ch - 48;
    
    ch = getchar();
    
    while(ch != 13)
    {
        if(!CheckChar(ch))
        {
            fprintf(stdout, "Did not meet number");
            stack = Add(newInt);
            memClear();
            exit (1);
        }
        
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
                fprintf(stdout, "Not enough memory!\n");
                memClear();
                exit (NOT_ENOUGH_MEMORY);
            }
            
            newInt -> next = NULL;
            newInt -> sign = list -> sign;
            list -> next = newInt;
            newInt -> value = 0;
            //list -> length++;
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
                fprintf(stdout, "Not enough memory!\n");
                memClear();
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

void ShowInt(IntList* value, int sign, int freeInt)
{
    if (sign && !(value -> length == 1 && value -> value == 0))
    {
        printf("-");
        sign--;
    }
    
    if (value -> next)
    {
        ShowInt(value -> next, sign, freeInt);
    }
    
    printf("%d", value -> value);
    
    if(freeInt)
    {
        free(value);
    }
    
    if(value -> length == 1580)
    {
        printf("");
    }
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
    else
    {
        prev -> length = this -> length;
    }
    
}

int Compare(IntList* a, IntList* b)
{
    int bool = -1;
    
    if(a->next)
    {
        bool = Compare(a -> next, b -> next);
    }
    if (a -> value == b -> value)
    {
        return bool;
    }
    if(bool == -1)
    {
        return a -> value > b -> value;
    }

    return bool;
}