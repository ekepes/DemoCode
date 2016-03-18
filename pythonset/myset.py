class Set:
  def __init__(self):
    self.mylist = list()

  def isEmpty(self):
    return len(self.mylist) == 0

  def add(self, item):
    if not self.contains(item):
      self.mylist.append(item)

  def contains(self, item):
    return item in self.mylist

  def size(self):
    return len(self.mylist)
