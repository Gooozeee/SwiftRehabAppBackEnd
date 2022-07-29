FROM python:3.9
WORKDIR /src
COPY ["VideoAnalysis/VideoAnalysisRabbitMQClient.py", "VideoAnalysis/"]
RUN pip install Pika
VOLUME /src/VideoAnalysis/UploadedVideos
CMD [ "python", "-u", "VideoAnalysis/VideoAnalysisRabbitMQClient.py"]
