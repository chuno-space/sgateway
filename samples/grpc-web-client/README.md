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
    or `npm install -D protoc-gen-grpc-web`

Add Directory contains 2 files to $PATH 

## Generate protobuf
mkdir ./generated

Window : Run in ./bin folder
```bash
./protoc.exe -I ../proto authservice.proto  --js_out=import_style=commonjs:../generated  --grpc-web_out=import_style=typescript,mode=grpcwebtext:../generated

# or using  `npm install -D protoc-gen-grpc-web` in node_module
./protoc.exe -I ../proto authservice.proto  --plugin=protoc-gen-grpc-web=../node_modules/protoc-gen-grpc-web/bin/protoc-gen-grpc-web.exe --js_out=import_style=commonjs:../generated  --grpc-web_out=import_style=typescript,mode=grpcwebtext:../generated
```

Linux : not test
```bash
protoc -I=. ../*.proto \
  --plugin=protoc-gen-grpc-web=./node_modules/protoc-gen-grpc-web/bin/protoc-gen-grpc-web \
  --js_out=import_style=commonjs:./generated \
  --grpc-web_out=import_style=typescript,mode=grpcwebtext:./generated
```

## Run dev

npm run dev

## Build

npm run build

## Run on built

https://www.npmjs.com/package/http-server
npm install --global http-server

npm run start