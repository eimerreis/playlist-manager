import crypto from "crypto";
import { EncryptionPassword } from "@env";

const algorithm = 'aes-256-ctr';

export const Encrypt = (text: string) => {
  const cipher = crypto.createCipher(algorithm, EncryptionPassword);
  let crypted = cipher.update(text,'utf8','hex')
  crypted += cipher.final('hex');
  return crypted;
}
 
export const Decrypt = (text: string) => {
  const decipher = crypto.createDecipher(algorithm, EncryptionPassword);
  let dec = decipher.update(text,'hex','utf8')
  dec += decipher.final('utf8');
  return dec;
}