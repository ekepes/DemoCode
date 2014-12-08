def probTest(limit):
    rolls = 0
    prob = 1.0

    while prob > limit:
        rolls += 1
        prob = (1.0 / 6.0) * ((5.0 / 6.0) ** (rolls - 1))
        #print prob

    return rolls

print probTest(0.05)
