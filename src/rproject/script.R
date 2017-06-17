#library(RODBC)

#channel <- odbcDriverConnect(settings$dbConnection)

#InputData <- sqlQuery(channel, iconv(paste(readLines('c:/users/grnanda/downloads/accessoriesmatcher_sql/rproject/sqlquery.sql', encoding = 'UTF-8', warn = FALSE), collapse = '\n'), from = 'UTF-8', to = 'ASCII', sub = ''))
#plot(InputData$userid, InputData$Id)


library(dplyr)
library(imager)
library(grid)

red = c(1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1)
green = c(0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0)
blue = c(0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0)

d = data.frame(r = red, g = green, b = blue)
freq = d %>% group_by(r, g, b) %>% dplyr::summarise(total = n())
freq[, 5] = c("Black", "Blue", "Green", "Red", "White")

r = red;
dim(r) = c(4, 4)
g = green;
dim(g) = c(4, 4)
b = blue;
dim(b) = c(4, 4)

rgbImage <- rgb(r, g, b)
dim(rgbImage) <- dim(r)

grid.raster(rgbImage, interpolate = F, width = 0.75, height = 0.75)

barplot(height = freq$total, names.arg = freq$V5, legend.text = "Frequency")


library(dplyr)
library(imager)

p = load.example("parrots")
rgb = color.at(p)

r = rgb[,, 1]
dim(r) = c(768 * 512, 1)
g = rgb[,, 2]
dim(g) = c(768 * 512, 1)
b = rgb[,, 3]
dim(b) = c(768 * 512, 1)

d = data.frame(r = r, g = g, b = b)

freq1 = d %>% group_by(r, g, b) %>% dplyr::summarise(total = n())
freq2 = d %>% group_by(r, g, b) %>% dplyr::summarise(total = n()) %>% dplyr::filter(total > 3)

barplot(height = freq$total, names.arg = freq2$V5, legend.text = "Frequency")

dim(freq1)

freq2 = d %>% group_by(r, g, b) %>% dplyr::summarise(total = n()) %>% dplyr::filter(total > 400)
dim(freq2)
barplot(height = freq2$total, legend.text = "Frequency")


