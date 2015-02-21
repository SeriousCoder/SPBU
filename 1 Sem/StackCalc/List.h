/*
    Author: Tarasenko Nikita
    Problem: "StackCalc", Stack.h
 
 */

#ifndef LIST_H
#define	LIST_H

#ifdef	__cplusplus
extern "C" {
#endif
    
#define NOT_ENOUGH_MEMORY 1

typedef struct
{
    int value;
    int sign; 
    int length;
    void *next;
}IntList;

    
typedef struct
{
    IntList* integer;
    void *next;
}StackList;


StackList* stack;

StackList* Add(IntList* value);

void memClear();


IntList* Inc(IntList* a, IntList* b);

IntList* Dec(IntList* a, IntList* b);

IntList* Mult(IntList* a, IntList* b);

IntList* Div(IntList* a, IntList* b);

IntList* Read(char ch, int sign);

void ShowInt(IntList* value, int sign, int free);

void EditSign(IntList* value);

void EditLength(IntList* prev, int leng);

int Compare(IntList* a, IntList* b);

void addIntList10(IntList* list, int value);

void addIntList(IntList* list, int value);


#ifdef	__cplusplus
}
#endif

#endif	/* LIST_H */

