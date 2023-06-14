mkdir Certificates
cd Certificates
openssl genrsa -out private.key
openssl req -new -key private.key -out csr.csr
openssl x509 -req -days 365 -in csr.csr -signkey private.key -out cert.crt
openssl pkcs12 -export -in cert.crt -inkey private.key -out cert.pfx
