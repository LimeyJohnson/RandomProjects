from math import pow
print 'I like Cheese'
exp = 15
nums = [1]

for iteration in range(0,1000):
    carry = 0
    for place in range(0, len(nums)):
        result = nums[place] * 2
        result = result + carry
        carry = 0
        if(result>9):
            carry = 1
            result = result - 10
            if(place == len(nums)-1):
                nums.append(1)
        nums[place] = result

print nums

sum = 0
for place in range(0, len(nums)):
    sum = sum + nums[place]

print sum




