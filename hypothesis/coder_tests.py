from hypothesis import given
from hypothesis.strategies import text
from coder import encode, decode

@given(text())
def test_decode_inverts_encode(s):
    assert decode(encode(s)) == s

if __name__ == '__main__':
    test_decode_inverts_encode()