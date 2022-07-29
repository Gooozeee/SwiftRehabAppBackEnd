#!/usr/bin/env python

import pika
import time
import base64

def callback(ch, method, properties, body):
    #print(" [x] %r:%r" % (method.routing_key, body))
    analyseData(body)


# Any data analysis will be done in this method
def analyseData(body):
    print("Saving video file to directory in python")
    with open('/src/VideoAnalysis/UploadedVideos/Video.mov', 'wb') as file:
        #print(body)
        file.write(body)
        file.close()


time.sleep(20)
connection = pika.BlockingConnection(pika.ConnectionParameters(host='rabbitmq'))
channel = connection.channel()

channel.exchange_declare(exchange='swift_rehab_app', exchange_type='topic')

result = channel.queue_declare('', exclusive=True)
queue_name = result.method.queue

channel.queue_bind(exchange='swift_rehab_app', queue=queue_name, routing_key='video.fromApp')

print(' [*] Waiting for tasks.')

channel.basic_consume(queue = queue_name, on_message_callback=callback, auto_ack=True)

channel.start_consuming()
