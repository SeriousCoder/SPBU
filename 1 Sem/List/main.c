/*
    Author: Tarasenko Nikita
    Problem: "List"
 
 */

#include <stdio.h>
#include <stdlib.h>

typedef struct
{
    long value;
    int *next_elem;
} list;

/*
int checking_an_idiot ()
{
    int a;
    char eol = ' ';
    while (scanf("%d%c", &a, &eol) != 2 || eol != '\n') 
    {
     printf("Wrong input. Repeat please.");
     scanf("%*[^\r^\n]");
    }
    
    if (scanf("%d%c", &a, &eol) == 1)
    {
        printf("Wrong input. \n");
        a = 0;
    }
    else
    {
        return a;
    }
    
}
*/

list* add_new_elem (int a, list* oldFirst)
{
    list* new_elem = (list*)malloc(sizeof(list));
    if(!new_elem)
    {
        printf("Not enough memory!");
        exit (NOT_ENOUGH_MEMORY);
    }
    new_elem -> value = a;
    new_elem -> next_elem = oldFirst;
    return new_elem;
}

void showList (list* firstElem)
{
    list* next = firstElem;
    while (next != NULL)
    {
        printf("%d ", next -> value);
        next = next -> next_elem;
    }
    printf("\n");
}

list* remove_elem (int a, list* beginList)
{
    list *next, *prev;
    if (beginList -> value == a)
    {
        next = beginList -> next_elem;
        free(beginList);
        return next;
    }
    else
    {
        prev = beginList;
        next = beginList -> next_elem;
        while (next != NULL)
        {
            if (next -> value == a)
            {
                prev -> next_elem = next -> next_elem;
                free(next);
                break;
            }
            else
            {
                prev = next;
                next = next -> next_elem;
            }
        }
    }
    return beginList;
}

void clearList(list* listBegin)
{
    list* next = listBegin -> next_elem;
    int bool = 1;
    
    while (bool)
    {
        free(listBegin);
        if (next == NULL)
        {
            bool = 0;
        }
        else
        {
            listBegin = next;
            next = listBegin -> next_elem;
        }
    }
}

int main() 
{
    list* beginList =  NULL;
    char inputComm = NULL;
    int inputInt;
    
    printf("Enter string (a <int> - add int; r <int> - remove int; p - write list; q - Quit): ");
    while (inputComm != 'q')
    {
        inputComm = getchar();
        switch (inputComm)
        {
            case 'a':
                inputInt = 0;
                inputComm = getchar();
                inputComm = getchar();
                while(inputComm != ' ')
                {
                    inputInt = 10 * inputInt + (int) inputComm - 48;
                    inputComm = getchar();
                }
                beginList = add_new_elem(inputInt, beginList);
                if (!beginList)
                {
                    return 0;
                }
                break;
            case 'r':
                inputInt = 0;
                inputComm = getchar();
                inputComm = getchar();
                while(inputComm != ' ')
                {
                    inputInt = 10 * inputInt + (int) inputComm - 48;
                    inputComm = getchar();
                }
                beginList = remove_elem(inputInt, beginList);
                break;
            case 'p':
                if (beginList != NULL)
                {
                    showList(beginList);
                }
                else
                {
                    printf("\n");
                }
                inputComm = getchar();
                break;
            default:
                if (beginList != NULL)
                {
                    clearList(beginList);   
                }
                break;
        }
    }
       
    return 0;
}

