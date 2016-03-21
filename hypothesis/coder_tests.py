from hypothesis import given, example
from hypothesis.strategies import text
from coder import encode, decode

@given(text())
@example('')
def test_decode_inverts_encode(s):
    assert decode(encode(s)) == s

if __name__ == '__main__':
    test_decode_inverts_encode()