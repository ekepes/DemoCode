import myset
import unittest

class SetTests(unittest.TestCase):
  def test_a_new_set_isempty(self):
    set = myset.Set()
    self.assertEqual(set.isEmpty(), True)

if __name__ == '__main__':
  unittest.main()