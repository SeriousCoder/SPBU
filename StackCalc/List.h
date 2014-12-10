#ifndef LIST_H
#define	LIST_H

#ifdef	__cplusplus
extern "C" {
#endif

typedef struct
{
    int value;
    int sign; 
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


#ifdef	__cplusplus
}
#endif

#endif	/* LIST_H */

