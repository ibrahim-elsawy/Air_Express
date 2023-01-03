import config from '../config'
import { Consumer, ConsumerSubscribeTopics, EachBatchPayload, Kafka, EachMessagePayload } from 'kafkajs'


interface ProducerMessage { 
	Email: string,
	name: string
};


export default class EmailConsumer {
	private kafkaConsumer: Consumer

	public constructor() {
		this.kafkaConsumer = this.createKafkaConsumer()
	}

	public async startConsumer(): Promise<void> {
		const topic: ConsumerSubscribeTopics = {
			topics: [config.apacheKafka.topicName],
			fromBeginning: false
		}

		try {
			await this.kafkaConsumer.connect()
			await this.kafkaConsumer.subscribe(topic)

			await this.kafkaConsumer.run({
				eachMessage: async (messagePayload: EachMessagePayload) => {
					const { topic, partition, message } = messagePayload
					const prefix = `${topic}[${partition} | ${message.offset}] / ${message.timestamp}`
					console.log(`- ${prefix} ${message.key}#${message.value}`)
				}
			})
			await this.shutdown();
		} catch (error) {
			console.log('Error: ', error)
		}
	}

	public async startBatchConsumer(): Promise<void> {
		const topic: ConsumerSubscribeTopics = {
			topics: [config.apacheKafka.topicName],
			fromBeginning: false
		}

		try {
			await this.kafkaConsumer.connect()
			await this.kafkaConsumer.subscribe(topic)
			await this.kafkaConsumer.run({
				eachBatch: async (eachBatchPayload: EachBatchPayload) => {
					const { batch } = eachBatchPayload
					for (const message of batch.messages) {
						const prefix = `${batch.topic}[${batch.partition} | ${message.offset}] / ${message.timestamp}`
						console.log(`- ${prefix} ${message.key}#${message.value}`)
					}
				}
			})
			// await this.shutdown();
		} catch (error) {
			console.log('Error: ', error)
		}
	}

	private async shutdown(): Promise<void> {
		await this.kafkaConsumer.disconnect()
	}

	private createKafkaConsumer(): Consumer {
		const kafka = new Kafka({
			clientId: config.apacheKafka.clientId,
			brokers: [config.apacheKafka.broker]
		})
		const consumer = kafka.consumer({ groupId: config.apacheKafka.broker })
		return consumer
	}
}