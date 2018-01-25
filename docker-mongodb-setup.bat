docker pull mvertes/alpine-mongo

docker build -t mvertes/alpine-mongo .

docker run -d --name mongo-dev -p 27017:27017 mvertes/alpine-mongo --smallfiles