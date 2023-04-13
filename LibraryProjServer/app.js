const express = require('express');
const nodemailer = require('nodemailer');
const app = express();
const port = 3000;

app.use(express.json());
app.post('/send-email', (req, res) => {
  // get the book data from the request body
  console.log("req",req.body)
  //console.log("res",res.body)
  const books = req.body.books;

  // create a transporter to send the email
  const transporter = nodemailer.createTransport({
    service: 'gmail',
    auth: {
      user: 'hilap94@gmail.com',
      pass: 'yamqiqnunfastbyq'
    }
  });

  // create the email message
  const message = {
    from: 'hilap94@gmail.com',
    to: 'hilap94@gmail.com',
    subject: 'Books I Like',
    text: books.map(book => `${book.title} / ${book.authors}`).join('\n')
  };

  // send the email
  transporter.sendMail(message, (err, info) => {
    if (err) {
      console.error(err);
      res.status(500).send('Error sending email');
    } else {
      console.log('Email sent:', info.response);
      res.status(200).send('Email sent successfully');
    }
  });
});

app.listen(port, () => {
  console.log(`Server listening at http://localhost:${port}`);
});