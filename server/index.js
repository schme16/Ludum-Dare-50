let port = process.env.PORT || 8080,
	express = require('express'),
	app = express(),
	{
		MongoClient,
		ObjectId,
		Server
	} = require('mongodb'),
	client = new MongoClient('mongodb+srv://gakkle:C9Le66YQXbhGtVyR@gakkle.53s7z.mongodb.net/test?authSource=admin&replicaSet=atlas-g830h4-shard-0&w=majority&readPreference=primary&appname=MongoDB%20Compass&retryWrites=true&ssl=true'),
	router = new express.Router(),
	compression = require('compression'),
	cors = require('cors')


//Compress all the data before sending
app.use(compression({level: 9}))

//Add cross origin support
app.use(cors())

//Serve the frontend 
app.use(express.static('builds'))


app.use('/post-score', (req, res) => {
	db.collection('data').insert({name: req.query.name.substring(0, 10), score: parseFloat(req.query.score)})
	//console.log(req.query)
	res.sendStatus(200)
})
app.use('/get-scores', (req, res) => {
	db.collection('data').find({}).sort({score: -1}).limit(10).toArray((err, docs) => {
	//console.log('Ya')
		res.json(docs)
	})
})

//Connect to the database
client.connect((err, database) => {

	//Shorthand the database
	db = client.db('ldjam-50')

	//Start the app listening
	app.listen(port, () => console.log(`App started on port:`, port))
})
	
	