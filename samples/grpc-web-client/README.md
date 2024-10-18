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
./protoc.exe -I ../proto authservice.proto  --js_out=import_style=commonjs:../generated  --grpc-web_out=import_style=commonjs+dts,mode=grpcwebtext:../generated

## Run dev

npm run dev

## Build
npm run build

## Run on built
https://www.npmjs.com/package/http-server
npm install --global http-server

npm run start