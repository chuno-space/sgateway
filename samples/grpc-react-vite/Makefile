.DEFAULT_GOAL := protogen

.PHONY: docker
docker:
	cd docker & docker build --tag protogen:latest .
	
.PHONY: protogen
protogen: ./proto
	docker run --name protoc --rm \
		-v $(CURDIR)/proto:/proto:ro \
		-v $(CURDIR)/src/grpc-web-client-gen:/gen protogen:latest \
	authservice.proto \
	echo.proto

.PHONY: taildev
taildev: ./proto
	docker run  --entrypoint "tail" \
		--name protoc --rm \
		-v $(CURDIR)/proto:/proto:ro \
		-v $(CURDIR)/src/grpc-web-client-gen:/gen protogen:latest -f /dev/null
	