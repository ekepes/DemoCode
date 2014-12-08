import random

def sampleQuizzes():
    trials = 10000
    successes = 0.0

    for i in range(trials):
        m1 = random.randint(50, 80)
        m2 = random.randint(60, 90)
        f = random.randint(55, 95)
        total = (0.25 * m1) + (0.25 * m2) + (0.50 * f)
        if (total >= 70) and (total <= 75):
            successes += 1

    return successes / trials

print sampleQuizzes()
