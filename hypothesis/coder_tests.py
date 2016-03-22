from hypothesis import given, example
from hypothesis.strategies import text
from coder import encode, decode
import unittest

class TestEncoding(unittest.TestCase):
  @given(text())
  @example('')
  def test_decode_inverts_encode(self, s):
    assert decode(encode(s)) == s

if __name__ == '__main__':
  unittest.main()