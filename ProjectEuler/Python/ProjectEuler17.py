singles = [3,3,5,4,4,3,5,5,4,3,6,6,8,8,7,7,9,8,8]
tens = [0,6,6,5,5,5,7,6,6]
hundred = 10
onethousand = 8
def letterCount(num):
    if(num == 0):
        return 0
    if(num < 20):
        return singles[num-1]
    if(num < 100):
        secondDigit = num/10
        return tens[secondDigit-1] + letterCount(num % 10)
    if(num == 100):
        return hundred
    if(num % 100 ==0):
        return singles[(num/100)-1] + 7
    if(num<1000):
        h = num/100
        return singles[h-1] + hundred + letterCount(num % 100)



#for num in range(1,100):
#    print str(num) + " "+str(letterCount(num))

print letterCount(300)
print len("threehundred")
sum = len("onethousand")
for num in range(1,1000):
    sum += letterCount(num)
print sum

#sum =0
#for num in range(100,201):
#    print str(num) + "->"+str(letterCount(num))
#    sum += letterCount(num)
#print sum