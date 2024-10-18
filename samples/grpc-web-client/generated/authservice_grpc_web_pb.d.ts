import * as grpcWeb from 'grpc-web';

import * as authservice_pb from './authservice_pb'; // proto import: "authservice.proto"


export class GreeterClient {
  constructor (hostname: string,
               credentials?: null | { [index: string]: string; },
               options?: null | { [index: string]: any; });

  sayHello(
    request: authservice_pb.HelloRequest,
    metadata: grpcWeb.Metadata | undefined,
    callback: (err: grpcWeb.RpcError,
               response: authservice_pb.HelloReply) => void
  ): grpcWeb.ClientReadableStream<authservice_pb.HelloReply>;

}

export class GreeterPromiseClient {
  constructor (hostname: string,
               credentials?: null | { [index: string]: string; },
               options?: null | { [index: string]: any; });

  sayHello(
    request: authservice_pb.HelloRequest,
    metadata?: grpcWeb.Metadata
  ): Promise<authservice_pb.HelloReply>;

}

