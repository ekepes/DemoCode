class Set:
  def __init__(self):
    self.empty = True

  def isEmpty(self):
    return self.empty

  def add(self, item):
    self.empty = False
