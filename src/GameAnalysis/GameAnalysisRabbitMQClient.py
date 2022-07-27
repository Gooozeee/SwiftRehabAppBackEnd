#!/usr/bin/env python

import pika

connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='rabbitmq'))
channel = connection.channel

channel.exchange_declare(exchange='swift_rehab_app', exchange_type='topic')

result = channel.queue_declare('', exclusive=True)
queue_name = result.method.queue

channel.queue_bind(exchange='swift_rehab_app', queue=queue_name, routing_key='game.score.fromApp')

print(' [*] Waiting for tasks. To exit press CTRL+C')

def callback(ch, method, properties, body):
    print(" [x] %r:%r" % (method.routing_key, body))

channel.basic_consume(queue = queue_name, on_message_callback=callback, auto_ack=True)

channel.start_consuming()
