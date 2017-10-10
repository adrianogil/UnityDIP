import cv2
import matplotlib.pyplot as plt
import numpy as np

img_path = "../UnityDIP/Assets/Textures/link.jpg"
img = cv2.imread(img_path)
img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)

crop_img = img[(5,5,5),:]

print("Open image with size " + str(img.shape))

# plt.axis("off")
# plt.imshow(crop_img, cmap='gray')
# plt.show()

# plt.axis("off")
img_signal = img[5,:]
plt.plot(range(0,img.shape[1]), img_signal)
plt.show()

near_neighbor_filter = [0, 0.25, 0.5, 0.25, 0]
plt.plot(range(0,len(near_neighbor_filter)), near_neighbor_filter)
plt.show()

convolved = np.convolve(img_signal, near_neighbor_filter)
plt.ion()
plt.plot(range(0,len(convolved)), convolved)
plt.draw()

plt.pause(0.001)
input("Press [enter] to continue.")
print('Finished')