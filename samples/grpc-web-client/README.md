# Grpc-Web
https://www.npmjs.com/package/grpc-web


## Install
npm install grpc-web
npm install google-protobuf
npm install -g protoc-gen-js ??

Download 
- protoc : https://github.com/protocolbuffers/protobuf/releases
    - protoc.exe
- protoc-gen-grpc-web: https://github.com/grpc/grpc-web/releases
    - protoc-gen-grpc-web.exe

Add Directory contains 2 files to $PATH 

## Generate
mkdir ./generated

Run in ./bin folder
./protoc.exe -I ../proto authservice.proto  --js_out=import_style=commonjs:../generated  --grpc-web_out=import_style=commonjs,mode=grpcwebtext:../generated

## Run
npx webpack --config webpack.config.js

https://www.npmjs.com/package/http-server
npm install --global http-server

http-server .