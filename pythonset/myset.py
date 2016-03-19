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

  def clear(self):
    self.mylist.clear()

  def remove(self, item):
    self.mylist.remove(item)

  def iterator(self):
    return self

  def __iter__(self):
    self.n = 0
    return self

  def __next__(self):
    if self.n < len(self.mylist):
      result = self.mylist[self.n]
      self.n += 1
      return result
    else:
      raise StopIteration
