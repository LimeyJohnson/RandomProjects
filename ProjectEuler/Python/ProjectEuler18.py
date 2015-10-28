max = 0
data = [];
def recurse(line, pos, sum):
    global max
    if(line < len(data)): #make sure we are not on the last line
        sum += data[line][pos]
        if(sum > max):
            max = sum
        recurse(line + 1, pos, sum)
        recurse(line + 1, pos + 1, sum)


lines=list(open("problem18"))

for line in lines:
    nums = line.replace('\n','').split(' ')
    lineArray = []
    for number in nums:
        lineArray.append(int(number))
    data.append(lineArray)

recurse(0,0,0)
print max