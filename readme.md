# mTLS Client Certificate Authentication for Microservices in ASP.NET

## Generating Certificates
To use mTLS you need to generate private keys and certificates for each project. This can easly be done using openssl.

1. Generate private keys
```
openssl genrsa -out private.key
```
2. Generate a certificate signing request
```
openssl req -new -key private.key -out csr.csr
```
3. Submit the CSR to a certificate authority (CA) like Let's Encrypt. Alternatively, you can self-sign the certificate for testing purposes 
```
openssl x509 -req -days 365 -in csr.csr -signkey private.key -out cert.crt
```
4. To use the certificate for HTTPS in ASP.NET we can bundle the private key with the x509 certificate in to a PKCS12 format
```
openssl pkcs12 -export -out cert.pfx -inkey private.key -in cert.crt
```