from myset import Set
import unittest

class SetTests(unittest.TestCase):
  def test_a_new_set_isempty(self):
    set = Set()
    self.assertEqual(set.isEmpty(), True)

  def test_add_one_item_makes_isempty_false(self):
    set = Set()
    set.add("one")
    self.assertEqual(set.isEmpty(), False)

  def test_add_one_item_can_retreive_it(self):
    set = Set()
    set.add("One")
    self.assertEqual(set.contains("One"), True)

  def test_add_several_items_returns_correct_size(self):
    set = Set()
    set.add("One")
    set.add("Two")
    set.add("Three")
    self.assertEqual(set.size(), 3)

  def test_add_item_twice_still_in_set_once(self):
    set = Set()
    set.add("One")
    set.add("One")
    self.assertEqual(set.size(), 1)

  def test_clear_removes_all_items_from_the_set(self):
    set = Set()
    set.add("One")
    set.add("Two")
    set.clear()
    self.assertEqual(set.size(), 0)

  def test_remove_removes_the_item(self):
    set = Set()
    set.add("One")
    set.add("Two")
    set.remove("One")
    self.assertEqual(set.contains("One"), False)
    self.assertEqual(set.contains("Two"), True)

  def test_can_iterate_through_the_set(self):
    set = Set()
    set.add("One")
    set.add("Two")
    set.add("Three")
    iterations = 0
    for item in set.iterator():
      iterations += 1
    self.assertEqual(iterations, 3)

  def test_can_iterate_through_the_set_and_get_each_item(self):
    set = Set()
    set.add(1)
    set.add(2)
    set.add(3)
    iterations = 0
    total = 0
    for item in set.iterator():
      iterations += 1
      total += item
    self.assertEqual(iterations, 3)
    self.assertEqual(total, 6)

if __name__ == '__main__':
  unittest.main()