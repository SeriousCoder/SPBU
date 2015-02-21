/*
    Author: Tarasenko Nikita
    Problem: "Float"
 
 */

#include <stdio.h>

int bitFloatToInt1(float fnum) 
{
    return *(int *)(&fnum);
}

int bitFloatToInt2(float fnum) 
{
    union {
        float fl;
        int unused;
    } val;
    val.fl = fnum;
    return val.unused;
}

void bitFloatToInt3(float fnum)
{
    union {
    float fnum;
    struct {
        unsigned int mantissa : 23;
        unsigned int exponent : 8;
        int sign : 1;
    } bitField;
    } fragm;

    fragm.fnum = fnum;
    
    int i = 0;
    float t = 0,
    mant = 1;
    for(i = 22, t = 0.5; i >= 0; i--, t /= 2) 
    {
        mant += !!((fragm.bitField.mantissa >> i) & 1) * t;
    }
        
    printf("Sign = %d\nExponent = %d\nFraction = %f", fragm.bitField.sign, fragm.bitField.exponent  - 127, mant);
}

void convert1and2(int intf)
{
    int sign = !!(intf >> 31);

    int exp = (intf >> 23) & 0xFF;

    float frac = 1,
          t = 0.5;
    int i = 0;
    for(i = 22, t = 0.5; i >= 0; i--, t /= 2) 
    {
        frac += !!((intf >> i) & 1) * t;
    }
    printf("Sign = %c\nExponent = %d\nFraction = %f", sign ? '1' : '0', exp - 127, frac);
}



int main(void) 
{
    printf("Enter a float number:");
    float f;
    scanf("%f", &f);

    int intf = 0;
    int b = 0;

    int k = 0;
    printf("Choose the way to translate:\n1. Pointers (enter 1)\n2. Union (enter 2)\n3. Union 2 (enter other)\n");
    scanf("%d", &k);
    switch (k) 
    {
        case 1: 
        {
            intf = bitFloatToInt1(f);
            break;
        }
        case 2: 
        {
            intf = bitFloatToInt2(f);
            break;
        }
        default: 
        {
            bitFloatToInt3(f);
            b = 1;
        }
    }


    if (!b)
    {
       convert1and2(intf);
    }
    
    return 0;
}