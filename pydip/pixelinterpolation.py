import cv2
import matplotlib.pyplot as plt

img_path = "../UnityDIP/Assets/Textures/link.jpg"
img = cv2.imread(img_path)
img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)

crop_img = img[(5,5,5),:]

print("Open image with size " + str(img.shape))

plt.axis("off")
plt.imshow(crop_img, cmap='gray')
plt.show()

# plt.axis("off")
plt.plot(range(0,img.shape[1]), img[5,:])
plt.show()