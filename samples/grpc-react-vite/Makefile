.PHONY: docker
docker:
	cd docker & docker build --tag protogen:latest .
	

.PHONY: protogen
protogen: ./proto
	docker run -v $(CURDIR)/proto:/proto:ro -v $(CURDIR)/src/grpc-web-client-gen:/gen protogen:latest \
	-I=./proto \
	--js_out=import_style=commonjs,binary:./gen \
	--grpc-web_out=import_style=typescript,mode=grpcweb:./gen \
	authservice.proto \
	echo.proto
