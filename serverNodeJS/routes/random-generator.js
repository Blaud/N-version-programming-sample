const express = require('express');
const router = express.Router();
const controller = require('../controllers/random-generator');

// host:port/api/random-generator/getRandom
router.get('/getRandom', controller.getRandom);

module.exports = router;
