/*
    Author: Tarasenko Nikita
    Problem: "StackCalc", List.h
 
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

IntList* Inc(IntList* a, IntList* b);

IntList* Dec(IntList* a, IntList* b);

IntList* Mult(IntList* a, IntList* b);

IntList* Div(IntList* a, IntList* b);

IntList* Read(char ch, int sign);

void ShowInt(IntList* value, int sign);

void EditSign(IntList* value);

void EditLength(IntList* prev, int leng);

int Compare(IntList* a, IntList* b);


#ifdef	__cplusplus
}
#endif

#endif	/* LIST_H */
