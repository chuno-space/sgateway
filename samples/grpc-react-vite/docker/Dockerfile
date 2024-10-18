
FROM ubuntu:22.04

ARG PROTOC_VERSION=28.2
ARG PROTOC_GEN_WEB_VERSION=1.5.0
ARG PROTOC_JS_VERSION=3.21.4

RUN apt-get update
RUN apt-get install curl unzip -y
RUN apt-get install -y procps

# Install protoc
# https://grpc.io/docs/protoc-installation/
#RUN curl -LO https://github.com/protocolbuffers/protobuf/releases/download/v3.15.8/protoc-3.15.8-linux-x86_64.zip
#RUN unzip protoc-3.15.8-linux-x86_64.zip

RUN curl -LO https://github.com/protocolbuffers/protobuf/releases/download/v${PROTOC_VERSION}/protoc-${PROTOC_VERSION}-linux-x86_64.zip
RUN unzip protoc-${PROTOC_VERSION}-linux-x86_64.zip
RUN rm protoc-${PROTOC_VERSION}-linux-x86_64.zip
RUN mv ./bin/protoc /usr/local/bin/.
RUN chmod +x /usr/local/bin/protoc


# Install protobuf javascript
RUN curl -LO https://github.com/protocolbuffers/protobuf-javascript/releases/download/v${PROTOC_JS_VERSION}/protobuf-javascript-${PROTOC_JS_VERSION}-linux-x86_64.zip
RUN unzip protobuf-javascript-${PROTOC_JS_VERSION}-linux-x86_64.zip
RUN rm protobuf-javascript-${PROTOC_JS_VERSION}-linux-x86_64.zip
RUN mv ./bin/protoc-gen-js /usr/local/bin/.
RUN chmod +x /usr/local/bin/protoc-gen-js

# Install protoc-gen-grpc-web
RUN curl -LO https://github.com/grpc/grpc-web/releases/download/${PROTOC_GEN_WEB_VERSION}/protoc-gen-grpc-web-${PROTOC_GEN_WEB_VERSION}-linux-x86_64

RUN mv protoc-gen-grpc-web-${PROTOC_GEN_WEB_VERSION}-linux-x86_64 /usr/local/bin/protoc-gen-grpc-web
RUN chmod +x /usr/local/bin/protoc-gen-grpc-web


# Prepare input/output directories
RUN mkdir proto
RUN mkdir gen

# Run the generation command
# https://github.com/grpc/grpc-web#typescript-support
# CMD protoc -I=./proto authservice.proto echo.proto \
#     --js_out=import_style=commonjs,binary:./gen \
#     --grpc-web_out=import_style=typescript,mode=grpcweb:./gen

# CMD protoc -I=. \
# --js_out=import_style=commonjs,binary:./gen \
# --grpc-web_out=import_style=typescript,mode=grpcweb:./gen ./proto/*.proto 

#ENTRYPOINT ["tail", "-f", "/dev/null"]
ENTRYPOINT ["protoc" ,"-I=./proto",  "--js_out=import_style=commonjs,binary:./gen","--grpc-web_out=import_style=typescript,mode=grpcweb:./gen"]
CMD ["authservice.proto"]
