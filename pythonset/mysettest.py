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

if __name__ == '__main__':
  unittest.main()