import base64
import sys
import os


def ResultHash(hash: str):
    base64_string = hash
    base64_bytes = base64_string.encode("UTF-8")

    sample_string_bytes = base64.b64decode(base64_bytes)
    sample_string = sample_string_bytes.decode("UTF-8")
    os.system('cls')
    while ((end := sample_string.find(",\"is_right\":1")) != -1):
        stt = sample_string[:end:].rfind("\"value\":") + 8
        print(f":  {eval(str(sample_string[stt:end:]))}")
        sample_string = sample_string[end + 15::]


print(f"\n\n{'     -'*5}\n\n")
ResultHash(sys.argv[1])
print("\n"*4)
input()